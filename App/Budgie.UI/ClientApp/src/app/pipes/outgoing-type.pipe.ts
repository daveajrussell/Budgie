import { Pipe, PipeTransform } from '@angular/core';
import { Outgoing, CategoryType } from 'app/models';

@Pipe({
  name: 'outgoingType'
})
export class OutgoingTypePipe implements PipeTransform {

  transform(outgoings: Outgoing[], categoryType: CategoryType): Outgoing[] {
    if (!outgoings || !categoryType) {
      return outgoings;
    }

    return outgoings.filter(outgoing => outgoing.category.type === categoryType);
  }

}
