import { Component, Inject } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html'
})
export class FetchDataComponent {
    public forecasts: any[];

    constructor(authService: AuthService, @Inject('API_URL') apiUrl: string) {
        authService.get(apiUrl + '/api/values').subscribe(result => {
            this.forecasts = result as any[];
        }, error => console.error(error));
    }
}
