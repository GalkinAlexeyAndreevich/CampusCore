import { Gender } from './enums/gender';
import { Student } from './students';

export interface StudentBlank {
	id: string | null;
	lastName: string | null;
	firstName: string | null;
	patronymic: string | null;
	gender: Gender | null;
	dateOfBirth: string | null;
	averageGrade: number | null;
	specialNotes: string[] | null;
	groupId: string | null;
}

export const StudentBlankUtils = {
	getDefault(): StudentBlank {
		return {
			id: null,
			lastName: null,
			firstName: null,
			patronymic: null,
			gender: null,
			dateOfBirth: null,
			averageGrade: null,
			specialNotes: null,
			groupId: null,
		};
	},

	fromStudent(student: Student): StudentBlank {
		return {
			id: student.id,
			lastName: student.lastName,
			firstName: student.firstName,
			patronymic: student.patronymic,
			gender: student.gender,
			dateOfBirth: student.dateOfBirth,
			averageGrade: student.averageGrade,
			specialNotes: student.specialNotes,
			groupId: student.groupId,
		};
	},
}
