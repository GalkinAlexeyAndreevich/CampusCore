import { mapToStudent, Student, StudentSource } from "./students";
import { mapToStudentGroup, StudentGroup, StudentGroupSource } from "../studentGroups/studentGroups";

export interface StudentDetail extends Student {
  group: StudentGroup;
}

export function mapToStudentDetails(data: StudentDetailSource[]): StudentDetail[] {
  return data.map(mapToStudentDetail);
}

export function mapToStudentDetail(data: StudentDetailSource): StudentDetail {

  return {
    ...mapToStudent(data),
    group: mapToStudentGroup(data.group),
  };
}

// Предполагаемые данные с api
interface StudentDetailSource extends StudentSource {
  group: StudentGroupSource;
}

