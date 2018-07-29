import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({
  name: 'dateFormat'
})
export class DateFormatPipe implements PipeTransform {

  transform(value: Date, format: string = 'DD/MM/YYYY'): any {
    return moment(value).format(format)
  }

}
