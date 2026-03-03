import { TrainingFormat } from "./enums/trainingFromat";

export interface StudentGroup {
	id: string;
	name: string;
	abbreviation: string;
	trainingFormat: TrainingFormat;
	studyStartYear: number;
	studyEndYear: number;
}

export function mapToStudentGroups(data: StudentGroupSource[]): StudentGroup[] {
  return data.map(mapToStudentGroup);
}

export function mapToStudentGroup(data: StudentGroupSource): StudentGroup {
  return {
    id: data.id,
    name: data.name,
    abbreviation: data.abbreviation,
    trainingFormat: data.trainingFormat,
    studyStartYear: data.studyStartYear,
    studyEndYear: data.studyEndYear,
  };
}

// Предполагаемые данные с api
interface StudentGroupSource {
  id: string;
  name: string;
  abbreviation: string;
  trainingFormat: TrainingFormat;
  studyStartYear: number;
  studyEndYear: number;
  [key: string]: unknown; 
}