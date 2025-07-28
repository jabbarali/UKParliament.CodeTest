import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Person } from '../models/person.model';

@Injectable({ providedIn: 'root' })
export class PersonService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  getAll(): Observable<Person[]> {
    return this.http.get<Person[]>(this.baseUrl + 'api/person');
  }
  getById(id: number): Observable<Person> {
    return this.http.get<Person>(this.baseUrl + `api/person/${id}`);
  }
  create(person: Person): Observable<Person> {
    return this.http.post<Person>(this.baseUrl + 'api/person', person);
  }
  update(person: Person): Observable<Person> {
    return this.http.put<Person>(this.baseUrl + `api/person/${person.id}`, person);
  }
  delete(id: number): Observable<void> {
    return this.http.delete<void>(this.baseUrl + `api/person/${id}`);
  }
}
