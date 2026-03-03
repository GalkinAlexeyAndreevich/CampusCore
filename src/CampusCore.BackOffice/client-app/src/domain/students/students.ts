import { Gender } from "./enums/gender";

export interface Student {
  id: string;
  lastName: string;
  firstName: string;
  patronymic: string | null;
  gender: Gender;
  dateOfBirth: string;
  averageGrade: number;
  specialNotes: string[] | null;
  groupId: string;
}

export function mapToStudents(data: StudentSource[]): Student[] {
  return data.map(mapToStudent);
}

export function mapToStudent(data: StudentSource): Student {
  return {
    id: data.id,
    lastName: data.lastName,
    firstName: data.firstName,
    patronymic: data.patronymic ?? null,
    gender: data.gender,
    dateOfBirth: data.dateOfBirth,
    averageGrade: data.averageGrade,
    specialNotes: Array.isArray(data.specialNotes) ? data.specialNotes : [],
    groupId: data.groupId,
  };
}

// Предполагаемые данные с api
interface StudentSource {
  id: string;
  lastName: string;
  firstName: string;
  patronymic?: string | null;
  gender: Gender;
  dateOfBirth: string;
  averageGrade: number;
  specialNotes?: string[];
  groupId: string;
  [key: string]: unknown; 
}