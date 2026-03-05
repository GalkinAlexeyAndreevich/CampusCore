import { Result } from "../../tools/types/results/result";
import { StudentBlank } from "./studentBlank";
import { mapToStudent, mapToStudents, Student } from "./students";
import { ScholarshipResponse } from "./studentScholarship";

export class StudentProvider {
  private static readonly headers: HeadersInit = [
    ["X-Requested-With", "XMLHttpRequest"],
    ["Content-Type", "application/json"],
  ];

  public static async saveStudent(studentBlank: StudentBlank): Promise<Result> {
    const response = await fetch("/api/students/save", {
      method: "POST",
      headers: this.headers,
      body: JSON.stringify(studentBlank),
    });
    const json = await response.json();

    return Result.get(json);
  }

  public static async getAllStudents(): Promise<Student[]> {
    const response = await fetch("/api/students", {
      method: "GET",
      headers: this.headers,
    });
    const json = await response.json();

    return mapToStudents(json);
  }

  public static async getStudentById(
    studentId: string,
  ): Promise<Student | null> {
    const response = await fetch(`/api/students/get_by_id?studentId=${studentId}`, {
      method: "GET",
      headers: this.headers,
    });
    const json = await response.json();

    return mapToStudent(json);
  }

  public static async calcScholarshipOnStudents(
    studentIds: string[],
  ): Promise<ScholarshipResponse[] | null> {
    const response = await fetch(`/api/students/calc-scholarships`, {
      method: "POST",
      headers: this.headers,
      body: JSON.stringify({
        studentIds: studentIds,
      }),
    });
    const json = await response.json();

    return json;
  }

  public static async markStudentAsRemoved(studentId: string): Promise<Result> {
    const response = await fetch(
      `/api/students/mark_as_deleted?studentId=${studentId}`,
      {
        method: "POST",
        headers: this.headers,
      },
    );
    const json = await response.json();

    return Result.get(json);
  }
}
