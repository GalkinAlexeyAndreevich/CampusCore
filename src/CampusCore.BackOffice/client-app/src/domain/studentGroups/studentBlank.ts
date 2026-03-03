import { TrainingFormat } from "./enums/trainingFromat";
import { StudentGroup } from "./studentGroups";

export interface StudentGroupBlank {
	id: string | null;
	name: string | null;
	abbreviation: string | null;
	trainingFormat: TrainingFormat | null;
	studyStartYear: number | null;
	studyEndYear: number | null;
}

export const StudentGroupBlankUtils = {
	getDefault(): StudentGroupBlank {
		return {
			id: null,
			name: null,
			abbreviation: null,
			trainingFormat: null,
			studyStartYear: null,
			studyEndYear: null,
		};
	},

	fromStudentGroup(studentGroup: StudentGroup): StudentGroupBlank {
		return {
			id: studentGroup.id,
			name: studentGroup.name,
			abbreviation: studentGroup.abbreviation,
			trainingFormat: studentGroup.trainingFormat,
			studyStartYear: studentGroup.studyStartYear,
			studyEndYear: studentGroup.studyEndYear,
		};
	},
}
