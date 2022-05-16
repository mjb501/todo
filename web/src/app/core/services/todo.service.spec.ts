import { HttpClient, HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { Observable } from 'rxjs';
import { of } from 'rxjs/internal/observable/of';
import { TodoItem } from '../models';

import { TodoService } from './todo.service';

describe('TodoService', () => {
  let service: TodoService;
  let httpClientSpy: jasmine.SpyObj<HttpClient>;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['get', 'post']);
    service = new TodoService(httpClientSpy);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should post when complete called', () => {
    httpClientSpy.post.and.returnValue(new Observable<string>());

    service.complete("");
    expect(httpClientSpy.post.calls.count())
      .withContext('single post')
      .toBe(1);
  });
});
