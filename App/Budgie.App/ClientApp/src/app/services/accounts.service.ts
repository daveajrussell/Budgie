import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
import { Subscription } from 'rxjs/Subscription';
import { catchError } from 'rxjs/operators';
import { BaseService } from 'app/services/base.service';
import { Account } from 'app/models';

@Injectable()
export class AccountsService extends BaseService {

  private accountsUrl: string = '';

  constructor(private http: HttpClient, @Inject('API_URL') baseUrl: string) {
    super();
    this.accountsUrl = `${baseUrl}/accounts`;
  }

  public getAccounts = (): Observable<Account[]> => {
    return this.http.get<Account[]>(this.accountsUrl)
      .pipe(catchError(this.handleError<Account[]>('getAccount')));
  }

  public addAccount = (account: Account): Observable<any> => {
    return this.http.put(this.accountsUrl, account, this.httpOptions)
      .pipe(catchError(this.handleError<Account>('addAccount')));
  }

  public editAccount = (account: Account): Observable<any> => {
    return this.http.patch(this.accountsUrl, account, this.httpOptions)
      .pipe(catchError(this.handleError<Account>('editAccount')));
  }
}