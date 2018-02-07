import { Pipe, PipeTransform } from '@angular/core';
import { Outgoing, CategoryType } from 'app/models';

@Pipe({
  name: 'outgoingTypeFilter'
})
export class OutgoingTypeFilterPipe implements PipeTransform {

  transform(outgoings: Outgoing[], categoryType: CategoryType): Outgoing[] {
    if (!outgoings || !categoryType) {
      return outgoings;
    }

    return outgoings.filter(outgoing => outgoing.category.type === categoryType);
  }

}
