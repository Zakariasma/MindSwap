import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-deck-card',
  templateUrl: './deck-card.component.html',
  styleUrls: ['./deck-card.component.css']
})
export class DeckCardComponent {
  @Input() title: string = '';

  @Output() editEvent = new EventEmitter<void>();
  @Output() deleteEvent = new EventEmitter<void>();

  edit(event: Event): void {
    event.stopPropagation();
    this.editEvent.emit();
  }

  delete(event: Event): void {
    event.stopPropagation();
    this.deleteEvent.emit();
  }
}
