import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";

import { DateFormatPipe, OutgoingTypePipe } from './index';

@NgModule({
    declarations: [DateFormatPipe, OutgoingTypePipe],
    imports: [CommonModule],
    exports: [DateFormatPipe, OutgoingTypePipe]
})

export class PipeModule { }