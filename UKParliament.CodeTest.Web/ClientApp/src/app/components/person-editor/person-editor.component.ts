import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { Person } from '../../models/person.model';
import { Department } from '../../models/department.model';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-person-editor',
  templateUrl: './person-editor.component.html',
  styleUrls: ['./person-editor.component.scss']
})
export class PersonEditorComponent implements OnChanges {
  @Input() person: Person | null = null;
  @Input() departments: Department[] = [];
  @Output() save = new EventEmitter<Person>();
  @Output() cancel = new EventEmitter<void>();
  @Output() delete = new EventEmitter<Person>();

  editPerson: Person = this.createEmptyPerson();
  errorMessage: string = '';

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['person']) {
      this.editPerson = this.person
        ? { ...this.person }
        : this.createEmptyPerson();
      this.errorMessage = '';
    }
  }

  createEmptyPerson(): Person {
    return {
      id: 0,
      firstName: '',
      lastName: '',
      dateOfBirth: '',
      departmentId: 0,
      email: ''
    };
  }

  onSubmit(form: NgForm) {
    if (form.invalid) return;
    this.save.emit(this.editPerson);
  }

  onDelete() {
    if (this.editPerson.id !== 0) {
      if (confirm('Are you sure you want to delete this person?')) {
        this.delete.emit(this.editPerson);
      }
    }
  }
}
