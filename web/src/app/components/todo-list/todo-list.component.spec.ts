import { HttpClientModule } from '@angular/common/http';
import { preserveWhitespacesDefault } from '@angular/compiler';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormBuilder } from '@angular/forms';
import { TodoService } from 'src/app/core/services/todo.service';
import { TodoItem } from 'src/app/core/models';

import { TodoListComponent } from './todo-list.component';
import { Observable } from 'rxjs';

describe('TodoListComponent', () => {
  let component: TodoListComponent;
  let fixture: ComponentFixture<TodoListComponent>;
  let service: TodoService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [FormBuilder],
      declarations: [ TodoListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TodoListComponent);
    service = TestBed.inject(TodoService);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should default showCompleted to false', () => {
    expect(component.showCompleted).toBeFalse();
  });

  it('should default submitting to false', () => {
    expect(component.submitting).toBeFalse();
  });

  it('should call service when submit called', () => {
    let serviceMock = spyOn(service, 'create').and.returnValue(new Observable<string>());
    component.submit();
    expect(serviceMock).toHaveBeenCalledTimes(1);
  });

  it('should call service when complete called', () => {
    let serviceMock = spyOn(service, 'complete').and.returnValue(new Observable<string>());
    let item: TodoItem = { id: "1", text: "", created: new Date() };
    component.complete(item);
    expect(serviceMock).toHaveBeenCalledTimes(1);
  });

  it('should toggleFilter should invert showCompleted', () => {
    let previous: boolean = component.showCompleted;
    let serviceMock = spyOn(service, 'toggleFilter');
    component.toggleFilter();
    expect(component.showCompleted).not.toBe(previous);
    expect(serviceMock).toHaveBeenCalledTimes(1);
  });
});
