import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-result-page',
  templateUrl: './result-page.component.html',
  styleUrls: ['./result-page.component.css']
})
export class ResultPageComponent {

  @Input() score: number | undefined;
  @Input() nombreQuestion: number | undefined;
  @Output() buttonPressed = new EventEmitter<boolean>();
  constructor(private router: Router, private route: ActivatedRoute) { }

  navigateTo(route: string) {
    this.router.navigate([route]).then(r => console.log(r));
  }

  buttonClicked() {
    this.buttonPressed.emit(true);
  }

}
