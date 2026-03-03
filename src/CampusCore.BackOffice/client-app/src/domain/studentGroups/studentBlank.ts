import { StudentGroup } from "./studentGroups";

export interface StudentGroupBlank {
	id: string | null;
	name: string | null;
	trainingFormat: string | null;
	studyStartYear: number | null;
	studyEndYear: number | null;
}

export const StudentGroupBlankUtils = {
	getDefault(): StudentGroupBlank {
		return {
			id: null,
			name: null,
			trainingFormat: null,
			studyStartYear: null,
			studyEndYear: null,
		};
	},

	fromStudentGroup(studentGroup: StudentGroup): StudentGroupBlank {
		return {
			id: studentGroup.id,
			name: studentGroup.name,
			trainingFormat: studentGroup.trainingFormat,
			studyStartYear: studentGroup.studyStartYear,
			studyEndYear: studentGroup.studyEndYear,
		};
	},
}
