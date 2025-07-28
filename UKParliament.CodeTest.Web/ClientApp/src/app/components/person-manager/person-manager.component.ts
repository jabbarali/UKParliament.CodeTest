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
  successMessage: string = '';
  showEditor = false;

  constructor(
    private personService: PersonService,
    private departmentService: DepartmentService
  ) { }

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
    this.showEditor = true;
    this.successMessage = '';
    this.errorMessage = '';
  }

  onAddNew() {
    this.selectedPerson = null;
    this.showEditor = false;
    this.successMessage = '';
    this.errorMessage = '';
    setTimeout(() => this.showEditor = true, 0);
  }

  onSave(person: Person) {
    this.successMessage = '';
    this.errorMessage = '';
    if (person.id === 0) {
      this.personService.create(person).subscribe({
        next: () => {
          this.loadPeople();
          this.selectedPerson = null;
          this.showEditor = false;
          this.successMessage = 'Person created successfully.';
        },
        error: err => {
          this.errorMessage = 'Failed to create person.';
          this.successMessage = '';
        }
      });
    } else {
      this.personService.update(person).subscribe({
        next: () => {
          this.loadPeople();
          this.selectedPerson = null;
          this.showEditor = false;
          this.successMessage = 'Person updated successfully.';
        },
        error: err => {
          this.errorMessage = 'Failed to update person.';
          this.successMessage = '';
        }
      });
    }
  }

  onCancel() {
    this.selectedPerson = null;
    this.showEditor = false;
  }

  onDelete(person: Person) {
    this.personService.delete(person.id).subscribe({
      next: () => {
        this.loadPeople();
        this.selectedPerson = null;
        this.showEditor = false;
        this.successMessage = 'Person deleted successfully.';
      },
      error: err => this.errorMessage = 'Failed to delete person.'
    });
  }

}
