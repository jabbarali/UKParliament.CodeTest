import { Department } from './department.model';

export interface Person {
  id: number;
  firstName: string;
  lastName: string;
  dateOfBirth: string; // ISO string
  departmentId: number;
  departmentName?: string;
  email: string;
  department?: Department;
}
