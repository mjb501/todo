import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, switchMap, tap } from 'rxjs';
import { TodoItem } from '../models';

@Injectable({
  providedIn: 'root'
})
export class TodoService {

  static readonly BasePath = 'https://localhost:7050/todo';

  private refreshList$ = new BehaviorSubject<boolean>(true);

  list$: Observable<TodoItem[]>;
  private showCompleted: boolean = false;

  constructor(private http: HttpClient) {

    this.list$ = this.list();

  }

  private list(): Observable<TodoItem[]> {
    return this.refreshList$.pipe(switchMap(
      _ => {
        let queryParams = { "includeCompleted": this.showCompleted };
        return this.http.get<TodoItem[]>(TodoService.BasePath + '/list', { params: queryParams });
      })
    );
  }

  create(text: string): Observable<string> {
    return this.http.post<string>(TodoService.BasePath + '/create', { text }).pipe(
      tap(_ => {
        this.refreshList$.next(false);
      })
    );
  }

  complete(id: string): Observable<string> {
    return this.http.post<string>(TodoService.BasePath + '/complete', { id }).pipe(
      tap(_ => {
        this.refreshList$.next(false);
      })
    );
  }

  toggleFilter(showCompleted: boolean): void {
    this.showCompleted = showCompleted;
    this.refreshList$.next(false);
  }
}
