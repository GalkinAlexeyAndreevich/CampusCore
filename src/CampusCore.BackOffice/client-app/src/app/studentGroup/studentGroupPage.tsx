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
import { StudentGroupEditorModal } from './modals/studentGroupEditorModal';
import { StudentGroup } from '../../domain/studentGroups/studentGroups';
import { StudentGroupProvider } from '../../domain/studentGroups/studentGroupProvider';
import { TrainingFormatUtils } from '../../domain/studentGroups/enums/trainingFromat';

type StudentGroupEditorModalState = {
	studentGroupId: string | null;
	isOpen: boolean;
};

interface RemoveStudentGroupConfirmModalState extends ConfirmModalState {
	studentGroupId: string | null;
}

export function StudentGroupPage() {
	const [studentGroups, setStudentGroups] = useState<StudentGroup[]>([]);

	const [studentGroupEditorModalState, setStudentGroupEditorModalState] = useState<StudentGroupEditorModalState>({
		studentGroupId: null,
		isOpen: false
	});
	const [removeStudentGroupConfirmModalState, setRemoveStudentGroupConfirmModalState] =
		useState<RemoveStudentGroupConfirmModalState>({ studentGroupId: null, ...ConfirmModalState.getClosed() });

	const [errorMessage, setErrorMessage] = useState<string | null>(null);

	useEffect(() => {
		loadStudentGroups();
	}, []);

	async function loadStudentGroups() {
		try {
			const studentGroups = await StudentGroupProvider.getAllStudentGroups();
			setStudentGroups(studentGroups);
		} catch (e) {
			const message = e instanceof Error ? e.message : 'Unknown error';
			setErrorMessage(message);
		}
	}

	function openStudentGroupEditorModal(studentGroupId?: string) {
		setStudentGroupEditorModalState({ studentGroupId: studentGroupId ?? null, isOpen: true });
	}

	function closeStudentGroupEditorModal(isEdited: boolean) {
		if (isEdited) loadStudentGroups();
		setStudentGroupEditorModalState({ studentGroupId: null, isOpen: false });
	}

	function openRemoveStudentGroupConfirmModal(studentGroupId: string, studentGroupName: string) {
		setRemoveStudentGroupConfirmModalState({
			studentGroupId,
			...ConfirmModalState.getOpen(`Вы действительно хотите удалить студенческую группу "${studentGroupName}"`)
		});
	}

	async function closeRemoveStudentGroupConfirmModal(isConfirmed: boolean) {
		if (isConfirmed) {
			if (removeStudentGroupConfirmModalState.studentGroupId == null) throw 'Cannot remove student group with StudentGroupId = null';

			const result = await StudentGroupProvider.markStudentGroupAsRemoved(removeStudentGroupConfirmModalState.studentGroupId);
			if (!result.isSuccess) {
				setErrorMessage(result.errors.map((error) => error.message).join('. '));
				return;
			}

			loadStudentGroups();
		}

		setRemoveStudentGroupConfirmModalState({ studentGroupId: null, ...ConfirmModalState.getClosed() });
	}

	function getCourse(group: StudentGroup): string {
		const now = new Date();
		const currentYear = now.getFullYear();
		// Начало года смотрим от сентября
		const academicYearStartYear = now.getMonth() + 1 >= 9 ? currentYear : currentYear - 1;
		const { studyStartYear, studyEndYear } = group;
		if (academicYearStartYear < studyStartYear) return "не начат";

		const course = academicYearStartYear - studyStartYear + 1;
		const lastCourse = Math.max(1, studyEndYear - studyStartYear);

		return lastCourse <= course ? "Выпустились" : String(course);
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
				<Typography variant='h6'>Студенческие группы</Typography>
				<Button variant='add' title='Создать' onClick={() => openStudentGroupEditorModal()} />
			</Paper>
			<Paper elevation={3} sx={{ height: 'calc(100% - 52px)' }}>
				<TableContainer sx={{ height: 'inherit' }}>
					<Table stickyHeader>
						<TableHead>
							<TableRow>
								<TableCell>Название</TableCell>
								<TableCell>Абревиатура</TableCell>
								<TableCell>Период обучения</TableCell>
								<TableCell>Курс</TableCell>
								<TableCell>Формат обучения</TableCell>
								<TableCell>Управление</TableCell>
							</TableRow>
						</TableHead>
						<TableBody>
							{studentGroups.length === 0 && (
								<TableRow>
									<TableCell colSpan={5}>Пусто</TableCell>
								</TableRow>
							)}
							{studentGroups.map((studentGroup) => {
								return (
									<TableRow key={`studentGroup__${studentGroup.id}`}>

										<TableCell width='20%'>{studentGroup.name}</TableCell>
										<TableCell width='20%'>{studentGroup.abbreviation}</TableCell>
										<TableCell width='15%'>{studentGroup.studyStartYear} - {studentGroup.studyEndYear}</TableCell>
										<TableCell width='15%'>{getCourse(studentGroup)}</TableCell>
										<TableCell width='15%'>
											{TrainingFormatUtils.getDisplayName(studentGroup.trainingFormat)}
										</TableCell>
										<TableCell>
											<Button
												type='icon'
												variant='edit'
												size='small'
												onClick={() => openStudentGroupEditorModal(studentGroup.id)}
											/>
											<Button
												type='icon'
												variant='remove'
												size='small'
												onClick={() => openRemoveStudentGroupConfirmModal(studentGroup.id, studentGroup.name)}
											/>
										</TableCell>
									</TableRow>
								);
							})}
						</TableBody>
					</Table>
				</TableContainer>
			</Paper>
			<StudentGroupEditorModal
				studentGroupId={studentGroupEditorModalState.studentGroupId}
				onClose={closeStudentGroupEditorModal}
				isOpen={studentGroupEditorModalState.isOpen}
			/>
			<ConfirmModal
				title={removeStudentGroupConfirmModalState.title}
				onClose={(isConfirmed) => closeRemoveStudentGroupConfirmModal(isConfirmed)}
				isOpen={removeStudentGroupConfirmModalState.isOpen}
			/>
			{String.isNotNullOrWhitespace(errorMessage) && (
				<Notification severity='error' message={errorMessage} onClose={() => setErrorMessage(null)} />
			)}
		</Container>
	);
}
