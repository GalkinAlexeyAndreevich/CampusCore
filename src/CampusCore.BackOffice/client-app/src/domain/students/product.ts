import { Page } from "../../tools/types/page";

export interface Student {
  id: string;
  lastName: string;
  firstName: string;
  patronymic: string | null;
  gender: number;
  dateOfBirth: string;
  averageGrade: number;
  specialNotes: string[];
  groupId: string;
}

export function mapToProductsPage(data: any): Page<Student> {
  return Page.convert(data, mapToProduct);
}

export function mapToProducts(data: any[]): Student[] {
  return data.map(mapToProduct);
}

export function mapToProduct(data: any): Student {
  return {
    id: data.id,
    lastName: data.lastName,
    firstName: data.firstName,
    patronymic: data.patronymic,
    gender: data.gender,
    dateOfBirth: data.dateOfBirth,
    averageGrade: data.averageGrade,
    specialNotes: data.specialNotes,
    groupId: data.groupId,
  };
}
