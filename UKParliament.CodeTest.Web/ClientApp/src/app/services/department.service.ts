import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Department } from '../models/department.model';

@Injectable({ providedIn: 'root' })
export class DepartmentService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  getAll(): Observable<Department[]> {
    return this.http.get<Department[]>(this.baseUrl + 'api/department');
  }
}
