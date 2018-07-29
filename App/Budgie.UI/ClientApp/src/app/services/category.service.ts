import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
import { Subscription } from 'rxjs/Subscription';
import { catchError } from 'rxjs/operators';
import { BaseService } from './base.service';
import { Category } from '../models';

@Injectable()
export class CategoryService extends BaseService {

  private categoriesUrl: string = '';

  constructor(private http: HttpClient, @Inject('API_URL') baseUrl: string) {
    super();
    this.categoriesUrl = `${baseUrl}/api/categories`;
  }

  public getCategories = (): Observable<Category[]> => {
    return this.http.get<Category[]>(this.categoriesUrl)
      .pipe(catchError(this.handleError<Category[]>('getCategories')));
  }

  public addCategory = (category: Category): Observable<any> => {
    return this.http.put(`${this.categoriesUrl}/add`, category, this.httpOptions)
      .pipe(catchError(this.handleError<Category>('addCategory')));
  }

  public editCategory = (category: Category): Observable<any> => {
    return this.http.patch(`${this.categoriesUrl}/edit`, category, this.httpOptions)
      .pipe(catchError(this.handleError<Category>('editCategory')));
  }

  public deleteCategory = (id: number): Observable<any> => {
    return this.http.delete(`${this.categoriesUrl}/${id}`, this.httpOptions)
      .pipe(catchError(this.handleError<Category>('deleteCategory')));
  }
}
