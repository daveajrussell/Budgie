import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
import { Subscription } from 'rxjs/Subscription';
import { catchError } from 'rxjs/operators';
import { BaseService } from 'app/services/base.service';
import { Category } from 'app/models';

@Injectable()
export class CategoryService extends BaseService {

  private categoriesUrl: string = '';

  constructor(private http: HttpClient, @Inject('API_URL') baseUrl: string) {
    super();
    this.categoriesUrl = `${baseUrl}/categories`;
  }

  public getCategories = (): Observable<Category[]> => {
    return this.http.get<Category[]>(this.categoriesUrl)
      .pipe(catchError(this.handleError<Category[]>('getCategories')));
  }

  public addCategory = (category: Category): Observable<any> => {
    return this.http.put(this.categoriesUrl, category, this.httpOptions)
      .pipe(catchError(this.handleError<Category>('addCategory')));
  }

  public editCategory = (category: Category): Observable<any> => {
    return this.http.patch(this.categoriesUrl, category, this.httpOptions)
      .pipe(catchError(this.handleError<Category>('editCategory')));
  }
}