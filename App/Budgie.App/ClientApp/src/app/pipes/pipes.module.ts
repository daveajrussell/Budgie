import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";

import { DateFormatPipe } from './date-format.pipe';
import { OutgoingTypePipe } from './outgoing-type.pipe';
import { AbsoluteNumberPipe } from './absolute-number.pipe';

@NgModule({
    declarations: [DateFormatPipe, OutgoingTypePipe, AbsoluteNumberPipe],
    imports: [CommonModule],
    exports: [DateFormatPipe, OutgoingTypePipe, AbsoluteNumberPipe]
})

export class PipeModule { }