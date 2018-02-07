import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
import { Subscription } from 'rxjs/Subscription';
import { catchError } from 'rxjs/operators';
import { BaseService } from 'app/services/base.service';
import { Budget, Outgoing, Transaction } from 'app/models';

@Injectable()
export class BudgetService extends BaseService {

  private accountsUrl: string = '';

  constructor(private http: HttpClient, @Inject('API_URL') baseUrl: string) {
    super();
    this.accountsUrl = `${baseUrl}/budgets`;
  }

  public getBudget = (year: number, month: number): Observable<Budget> => {
    return this.http.get<Budget>(this.accountsUrl)
      .pipe(catchError(this.handleError<Budget>('getBudget')));
  }

  public editOutgoing = (outgoing: Outgoing): Observable<any> => {
    return this.http.patch(this.accountsUrl, outgoing, this.httpOptions)
      .pipe(catchError(this.handleError<Outgoing>('editOutgoing')));
  }

  public addTransaction = (transaction: Transaction): Observable<any> => {
    return this.http.put(this.accountsUrl, transaction, this.httpOptions)
      .pipe(catchError(this.handleError<Transaction>('addTransaction')));
  }

  public editTransaction = (transaction: Transaction): Observable<any> => {
    return this.http.patch(this.accountsUrl, transaction, this.httpOptions)
      .pipe(catchError(this.handleError<Transaction>('editTransaction')));
  }

  public deleteTransaction = (transaction: Transaction): Observable<any> => {
    return this.http.delete(`${this.accountsUrl}/${transaction.id}`, this.httpOptions)
      .pipe(catchError(this.handleError<Transaction>('deleteTransaction')));
  }
}
