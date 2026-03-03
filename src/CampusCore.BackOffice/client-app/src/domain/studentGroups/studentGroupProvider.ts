import { Result } from '../../tools/types/results/result';
import { mapToStudentGroup, mapToStudentGroups, StudentGroup } from './studentGroups';
import { StudentGroupBlank } from './studentBlank';

export class StudentGroupProvider {
	private static readonly headers: HeadersInit = [
		['X-Requested-With', 'XMLHttpRequest'],
		['Content-Type', 'application/json']
	];

	public static async saveStudentGroup(studentGroupBlank: StudentGroupBlank): Promise<Result> {
		const response = await fetch('/student-groups/save', {
			method: 'POST',
			headers: this.headers,
			body: JSON.stringify(studentGroupBlank)
		});
		const json = await response.json();

		return Result.get(json);
	}

	public static async getAllStudentGroups(): Promise<StudentGroup[]> {
		const response = await fetch('/student-groups', {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();

		return mapToStudentGroups(json);
	}

	public static async getStudentGroupById(studentGroupId: string): Promise<StudentGroup | null> {
		const response = await fetch(`/student-groups/get_by_id?groupId=${studentGroupId}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();

		return mapToStudentGroup(json);
	}

	public static async markStudentGroupAsRemoved(studentGroupId: string): Promise<Result> {
		const response = await fetch(`/student-groups/mark_as_deleted?groupId=${studentGroupId}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();

		return Result.get(json);
	}
}
