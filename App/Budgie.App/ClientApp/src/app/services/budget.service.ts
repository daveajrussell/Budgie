import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
import { Subscription } from 'rxjs/Subscription';
import { catchError } from 'rxjs/operators';
import { BaseService } from 'app/services/base.service';
import { Outgoing, Transaction } from 'app/models';

@Injectable()
export class BudgetService extends BaseService {

  private accountsUrl: string = '';

  constructor(private http: HttpClient, @Inject('API_URL') baseUrl: string) {
    super();
    this.accountsUrl = `${baseUrl}/budgets`;
  }

  public getBudget(year: number, month: number) {

  }

  public editOutgoing(outgoing: Outgoing) {

  }

  public addTransaction(transaction: Transaction) {

  }

  public editTransaction(transaction: Transaction) {

  }
}
