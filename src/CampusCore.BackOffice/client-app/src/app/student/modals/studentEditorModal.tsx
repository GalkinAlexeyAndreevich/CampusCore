import React, { useEffect, useState } from 'react';
import { Button } from '../../../shared/components/buttons/button';
import { Input } from '../../../shared/components/inputs/input';
import { Modal } from '../../../shared/components/modals/modal';
import { Notification } from '../../../shared/components/notification';
import { Enum } from '../../../tools/types/enum';
import { StudentBlank, StudentBlankUtils } from '../../../domain/students/studentBlank';
import { Student } from '../../../domain/students/students';
import { Gender, GenderUtils } from '../../../domain/students/enums/gender';
import { StudentProvider } from '../../../domain/students/studentProvider';
import { StudentGroup } from '../../../domain/studentGroups/studentGroups';
import { StudentGroupProvider } from '../../../domain/studentGroups/studentGroupProvider';

interface Props {
	studentId: string | null;
	onClose: (isEdited: boolean) => void;
	isOpen: boolean;
}

export function StudentEditorModal(props: Props) {
	const [studentBlank, setStudentBlank] = useState<StudentBlank>(StudentBlankUtils.getDefault());
	const [groups, setGroups] = useState<StudentGroup[]>([]);
	const [errorMessage, setErrorMessage] = useState<string | null>(null);

	useEffect(() => {
		if (!props.isOpen) return;

		async function load() {
			let blank: StudentBlank | null = null;

			if (props.studentId != null) {
				const student: Student | null = await StudentProvider.getStudentById(props.studentId);
				if (student == null) throw 'Student is null';
				blank = StudentBlankUtils.fromStudent(student);
			}

			const groups = await StudentGroupProvider.getAllStudentGroups();
			setGroups(groups);
			setStudentBlank(blank ?? StudentBlankUtils.getDefault());
		}

		load();

		return () => {
			setStudentBlank(StudentBlankUtils.getDefault());
			setErrorMessage(null);
		};
	}, [props.isOpen, props.studentId]);

	async function saveStudent() {
		const result = await StudentProvider.saveStudent(studentBlank);
		if (!result.isSuccess) {
			setErrorMessage(result.getErrorString());
			return;
		}

		props.onClose(true);
	}

	const specialNotesText = studentBlank.specialNotes?.join('\n') ?? '';

	function setSpecialNotesFromText(text: string | null) {
		if(text == null || text.trim() === '') {
			setStudentBlank((prev) => ({ ...prev, specialNotes: null }));
			return;
		}

		const notes = text.split('\n').map((s) => s.trim()).filter((s) => s.length > 0);
		setStudentBlank((prev) => ({ ...prev, specialNotes: notes }));
	}

	const selectedGroup = groups.find((g) => g.id === studentBlank.groupId) ?? null;

	return (
		<>
			<Modal onClose={() => props.onClose(false)} isOpen={props.isOpen}>
				<Modal.Header onClose={() => props.onClose(false)}>Редактор студента</Modal.Header>
				<Modal.Body
					sx={{
						maxWidth: '800px',
						minWidth: '600px',
						display: 'flex',
						flexDirection: 'column',
						gap: '12px'
					}}>
					<Input
						variant='text'
						title='Фамилия'
						value={studentBlank.lastName}
						onChange={(lastName) => setStudentBlank((prev) => ({ ...prev, lastName }))}
						required
					/>
					<Input
						variant='text'
						title='Имя'
						value={studentBlank.firstName}
						onChange={(firstName) => setStudentBlank((prev) => ({ ...prev, firstName }))}
						required
					/>
					<Input
						variant='text'
						title='Отчество'
						value={studentBlank.patronymic}
						onChange={(patronymic) => setStudentBlank((prev) => ({ ...prev, patronymic }))}
					/>
					<Input
						variant='select'
						title='Пол'
						options={Enum.getNumberValues<Gender>(Gender)}
						getOptionLabel={(option) => GenderUtils.getDisplayName(option)}
						isOptionEqualToValue={(a, b) => a === b}
						value={studentBlank.gender}
						onChange={(gender) => setStudentBlank((prev) => ({ ...prev, gender }))}
						required
					/>
					<Input
						variant='date'
						title='Дата рождения'
						value={studentBlank.dateOfBirth}
						onChange={(dateOfBirth) => setStudentBlank((prev) => ({ ...prev, dateOfBirth }))}
						required
					/>
					<Input
						variant='number'
						title='Средний балл'
						value={studentBlank.averageGrade}
						onChange={(averageGrade) => setStudentBlank((prev) => ({ ...prev, averageGrade }))}
						isAvailableFractionValue
						step={0.01}
						required
					/>
					<Input
						variant='select'
						title='Группа'
						options={groups}
						getOptionLabel={(option) => option.name}
						isOptionEqualToValue={(a, b) => a.id === b.id}
						value={selectedGroup}
						onChange={(group) => setStudentBlank((prev) => ({ ...prev, groupId: group?.id ?? null }))}
						required
					/>
					<Input
						variant='text-area'
						title='Особые пометки (каждая с новой строки)'
						minRows={3}
						value={specialNotesText}
						onChange={(v) => setSpecialNotesFromText(v)}
					/>
				</Modal.Body>
				<Modal.Footer>
					<Button variant='save' onClick={() => saveStudent()} />
				</Modal.Footer>
			</Modal>
			{String.isNotNullOrWhitespace(errorMessage) && (
				<Notification severity='error' message={errorMessage} onClose={() => setErrorMessage(null)} />
			)}
		</>
	);
}
