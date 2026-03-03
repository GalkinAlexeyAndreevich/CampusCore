import React, { useEffect, useState } from 'react';
import { Button } from '../../../shared/components/buttons/button';
import { Input } from '../../../shared/components/inputs/input';
import { Modal } from '../../../shared/components/modals/modal';
import { Notification } from '../../../shared/components/notification';
import { Enum } from '../../../tools/types/enum';
import { StudentGroupBlank, StudentGroupBlankUtils } from '../../../domain/studentGroups/studentBlank';
import { StudentGroup } from '../../../domain/studentGroups/studentGroups';
import { TrainingFormat, TrainingFormatUtils } from '../../../domain/studentGroups/enums/trainingFromat';
import { StudentGroupProvider } from '../../../domain/studentGroups/studentGroupProvider';

interface Props {
	studentGroupId: string | null;
	onClose: (isEdited: boolean) => void;
	isOpen: boolean;
}

export function StudentGroupEditorModal(props: Props) {
	const [studentGroupBlank, setStudentGroupBlank] = useState<StudentGroupBlank>(StudentGroupBlankUtils.getDefault());
	const [errorMessage, setErrorMessage] = useState<string | null>(null);

	useEffect(() => {
		if (!props.isOpen) return;

		async function loadStudentGroupBlank() {
			let studentGroupBlank: StudentGroupBlank | null = null;

			if (props.studentGroupId != null) {
				const studentGroup: StudentGroup | null = await StudentGroupProvider.getStudentGroupById(props.studentGroupId);
				if (studentGroup == null) throw 'Student group is null';

				studentGroupBlank = StudentGroupBlankUtils.fromStudentGroup(studentGroup);
			}

			setStudentGroupBlank(studentGroupBlank ?? StudentGroupBlankUtils.getDefault());
		}

		loadStudentGroupBlank();

		return () => {
			setStudentGroupBlank(StudentGroupBlankUtils.getDefault());
			setErrorMessage(null);
		};
	}, [props.isOpen, props.studentGroupId]);

	async function saveStudentGroup() {
		const result = await StudentGroupProvider.saveStudentGroup(studentGroupBlank);
		if (!result.isSuccess) {
			setErrorMessage(result.getErrorString());
			return;
		}

		props.onClose(true);
	}

	return (
		<>
			<Modal onClose={() => props.onClose(false)} isOpen={props.isOpen}>
				<Modal.Header onClose={() => props.onClose(false)}>Редактор студенческой группы</Modal.Header>
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
						title='Введите название'
						value={studentGroupBlank.name}
						onChange={(name) => setStudentGroupBlank((studentGroupBlank) => ({ ...studentGroupBlank, name }))}
						required
					/>
					<Input
						variant='text-area'
						title='Введите аббревиатуру'
						minRows={2}
						value={studentGroupBlank.abbreviation}
						onChange={(abbreviation) =>
							setStudentGroupBlank((studentGroupBlank) => ({ ...studentGroupBlank, abbreviation }))
						}
					/>
					<Input
						variant='number'
						title='Введите год начала обучения'
						value={studentGroupBlank.studyStartYear}
						onChange={(studyStartYear) => setStudentGroupBlank((studentGroupBlank) => ({ ...studentGroupBlank, studyStartYear }))}
						required
					/>
					<Input
						variant='number'
						title='Введите год окончания обучения'
						value={studentGroupBlank.studyEndYear}
						onChange={(studyEndYear) => setStudentGroupBlank((studentGroupBlank) => ({ ...studentGroupBlank, studyEndYear }))}
						required
					/>
					<Input
						variant='select'
						title='Выберите формат обучения'
						options={Enum.getNumberValues<TrainingFormat>(TrainingFormat)}
						getOptionLabel={(option) => TrainingFormatUtils.getDisplayName(option)}
						isOptionEqualToValue={(a, b) => a === b}
						value={studentGroupBlank.trainingFormat}
						onChange={(trainingFormat) => setStudentGroupBlank((studentGroupBlank) => ({ ...studentGroupBlank, trainingFormat }))}
						required
					/>
				</Modal.Body>
				<Modal.Footer>
					<Button variant='save' onClick={() => saveStudentGroup()} />
				</Modal.Footer>
			</Modal>
			{String.isNotNullOrWhitespace(errorMessage) && (
				<Notification severity='error' message={errorMessage} onClose={() => setErrorMessage(null)} />
			)}
		</>
	);
}
