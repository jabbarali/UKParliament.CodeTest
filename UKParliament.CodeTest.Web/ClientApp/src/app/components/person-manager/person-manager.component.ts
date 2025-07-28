import { Component, OnInit } from '@angular/core';
import { Person } from '../../models/person.model';
import { Department } from '../../models/department.model';
import { PersonService } from '../../services/person.service';
import { DepartmentService } from '../../services/department.service';

@Component({
  selector: 'app-person-manager',
  templateUrl: './person-manager.component.html',
  styleUrls: ['./person-manager.component.scss']
})
export class PersonManagerComponent implements OnInit {
  people: Person[] = [];
  departments: Department[] = [];
  selectedPerson: Person | null = null;
  errorMessage: string = '';

  constructor(
    private personService: PersonService,
    private departmentService: DepartmentService
  ) {}

  ngOnInit(): void {
    this.loadPeople();
    this.departmentService.getAll().subscribe(depts => this.departments = depts);
  }

  loadPeople() {
    this.personService.getAll().subscribe({
      next: people => this.people = people,
      error: err => this.errorMessage = 'Failed to load people.'
    });
  }

  onSelectPerson(person: Person) {
    this.selectedPerson = person;
  }

  onAddNew() {
    this.selectedPerson = null;
  }

  onSave(person: Person) {
    if (person.id === 0) {
      this.personService.create(person).subscribe({
        next: () => {
          this.loadPeople();
          this.selectedPerson = null;
        },
        error: err => this.errorMessage = 'Failed to create person.'
      });
    } else {
      this.personService.update(person).subscribe({
        next: () => {
          this.loadPeople();
          this.selectedPerson = null;
        },
        error: err => this.errorMessage = 'Failed to update person.'
      });
    }
  }

  onCancel() {
    this.selectedPerson = null;
  }
}
