import {
	Container,
	Paper,
	Table,
	TableBody,
	TableCell,
	TableContainer,
	TableHead,
	TableRow,
	Typography
} from '@mui/material';
import React, { useEffect, useState } from 'react';
import { Button } from '../../shared/components/buttons/button';
import { ConfirmModal } from '../../shared/components/modals/confirmModal';
import { Notification } from '../../shared/components/notification';
import { ConfirmModalState } from '../../shared/types/confirmModalState';
import { StudentEditorModal } from './modals/studentEditorModal';
import { Student } from '../../domain/students/students';
import { StudentProvider } from '../../domain/students/studentProvider';
import { GenderUtils } from '../../domain/students/enums/gender';
import { StudentGroupProvider } from '../../domain/studentGroups/studentGroupProvider';
import { StudentGroup } from '../../domain/studentGroups/studentGroups';

type StudentEditorModalState = {
	studentId: string | null;
	isOpen: boolean;
};

interface RemoveStudentConfirmModalState extends ConfirmModalState {
	studentId: string | null;
}

export function StudentPage() {
	const [students, setStudents] = useState<Student[]>([]);
	const [groups, setGroups] = useState<StudentGroup[]>([]);

	const [studentEditorModalState, setStudentEditorModalState] = useState<StudentEditorModalState>({
		studentId: null,
		isOpen: false
	});
	const [removeStudentConfirmModalState, setRemoveStudentConfirmModalState] =
		useState<RemoveStudentConfirmModalState>({ studentId: null, ...ConfirmModalState.getClosed() });

	const [errorMessage, setErrorMessage] = useState<string | null>(null);

	useEffect(() => {
		loadStudents();
		loadGroups();
	}, []);

	async function loadStudents() {
		try {
			const students = await StudentProvider.getAllStudents();
			setStudents(students);
		} catch (e) {
			const message = e instanceof Error ? e.message : 'Unknown error';
			setErrorMessage(message);
		}
	}

	async function loadGroups() {
		try {
			const groups = await StudentGroupProvider.getAllStudentGroups();
			setGroups(groups);
		} catch {
			// ignore
		}
	}

	function openStudentEditorModal(studentId?: string) {
		setStudentEditorModalState({ studentId: studentId ?? null, isOpen: true });
	}

	function closeStudentEditorModal(isEdited: boolean) {
		if (isEdited) loadStudents();
		setStudentEditorModalState({ studentId: null, isOpen: false });
	}

	function openRemoveStudentConfirmModal(studentId: string, studentName: string) {
		setRemoveStudentConfirmModalState({
			studentId,
			...ConfirmModalState.getOpen(`Вы действительно хотите удалить студента "${studentName}"`)
		});
	}

	async function closeRemoveStudentConfirmModal(isConfirmed: boolean) {
		if (isConfirmed) {
			if (removeStudentConfirmModalState.studentId == null) throw 'Cannot remove student with studentId = null';

			const result = await StudentProvider.markStudentAsRemoved(removeStudentConfirmModalState.studentId);
			if (!result.isSuccess) {
				setErrorMessage(result.errors.map((error) => error.message).join('. '));
				return;
			}

			loadStudents();
		}

		setRemoveStudentConfirmModalState({ studentId: null, ...ConfirmModalState.getClosed() });
	}

	function getCourse(groupId: string): number {
		const group = groups.find((g) => g.id === groupId);
		if (group == null) return 0;

		const now = new Date();
		const currentYear = now.getFullYear();
		// Начало года смотрим от сентября
		const academicYearStartYear = now.getMonth() + 1 >= 9 ? currentYear : currentYear - 1;
		const { studyStartYear, studyEndYear } = group;
		if (academicYearStartYear < studyStartYear) return 0;

		const course = academicYearStartYear - studyStartYear + 1;
		const lastCourse = Math.max(1, studyEndYear - studyStartYear);

		return Math.min(course, lastCourse);
	}

	function getGroupName(groupId: string): string {
		const group = groups.find((g) => g.id === groupId);
		return group?.name || '-';
	}

	function getFio(student: Student): string {
		return `${student.lastName} ${student.firstName} ${student.patronymic ?? ''}`;
	}

	function formatDateOfBirth(value: string | null | undefined): string {
		if (String.isNullOrWhitespace(value)) return '—';
		const date = new Date(value);
		const formatted = date.toLocaleDateString('ru-RU', {
			day: '2-digit',
			month: '2-digit',
			year: 'numeric',
		});
		return formatted;
	}

	function formatAge(value: string | null | undefined): string {
		if (String.isNullOrWhitespace(value)) return '—';
		const date = new Date(value);
		const age = new Date().getFullYear() - date.getFullYear();
		return `${age} (${formatDateOfBirth(value)})`;
	}

	return (
		<Container
			sx={{ height: '100%', display: 'flex', flexDirection: 'column', gap: '12px' }}
			maxWidth={false}
			disableGutters>
			<Paper
				elevation={3}
				sx={{
					display: 'flex',
					alignItems: 'center',
					justifyContent: 'space-between',
					paddingX: '12px',
					paddingY: '6px'
				}}>
				<Typography variant='h6'>Студенты</Typography>
				<Button variant='add' title='Создать' onClick={() => openStudentEditorModal()} />
			</Paper>
			<Paper elevation={3} sx={{ height: 'calc(100% - 52px)' }}>
				<TableContainer sx={{ height: 'inherit' }}>
					<Table stickyHeader>
						<TableHead>
							<TableRow>
								<TableCell>ФИО</TableCell>
								<TableCell>Пол</TableCell>
								<TableCell>Возраст</TableCell>
								<TableCell>Средний балл</TableCell>
								<TableCell>Группа</TableCell>
								<TableCell>Курс</TableCell>
								<TableCell>Особые отметки</TableCell>
								<TableCell>Управление</TableCell>
							</TableRow>
						</TableHead>
						<TableBody>
							{students.length === 0 && (
								<TableRow>
									<TableCell colSpan={8}>Пусто</TableCell>
								</TableRow>
							)}
							{students.map((student) => (
								<TableRow key={`student__${student.id}`}>
									<TableCell width='20%'>{getFio(student)}</TableCell>
									<TableCell width='10%'>{GenderUtils.getDisplayName(student.gender)}</TableCell>
									<TableCell width='12%'>{formatAge(student.dateOfBirth)}</TableCell>
									<TableCell width='10%'>{student.averageGrade}</TableCell>
									<TableCell width='13%'>{getGroupName(student.groupId)}</TableCell>
									<TableCell width='13%'>{getCourse(student.groupId)}</TableCell>
									<TableCell width='13%'>{student.specialNotes}</TableCell>
									<TableCell>
										<Button
											type='icon'
											variant='edit'
											size='small'
											onClick={() => openStudentEditorModal(student.id)}
										/>
										<Button
											type='icon'
											variant='remove'
											size='small'
											onClick={() =>
												openRemoveStudentConfirmModal(
													student.id,
													`${student.lastName} ${student.firstName}`
												)
											}
										/>
									</TableCell>
								</TableRow>
							))}
						</TableBody>
					</Table>
				</TableContainer>
			</Paper>
			<StudentEditorModal
				studentId={studentEditorModalState.studentId}
				onClose={closeStudentEditorModal}
				isOpen={studentEditorModalState.isOpen}
			/>
			<ConfirmModal
				title={removeStudentConfirmModalState.title}
				onClose={(isConfirmed) => closeRemoveStudentConfirmModal(isConfirmed)}
				isOpen={removeStudentConfirmModalState.isOpen}
			/>
			{String.isNotNullOrWhitespace(errorMessage) && (
				<Notification severity='error' message={errorMessage} onClose={() => setErrorMessage(null)} />
			)}
		</Container>
	);
}
