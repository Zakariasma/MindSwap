import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Router} from "@angular/router";
import {ResultService} from "../../_services/result.service";
import {Result} from "../../models/result";

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.css']
})
export class ResultComponent {
  @Input() score!: number ;
  @Input() nombreQuestion!: number;
  @Output() buttonPressed = new EventEmitter<boolean>();
  constructor(private router: Router, private ResultService: ResultService ) { }

  ngOnInit(): void {
    this.insertScort();
  }
  navigateTo(route: string) {
    this.router.navigate([route]).then(r => console.log(r));
  }

  buttonClicked() {
    this.buttonPressed.emit(true);
  }

  insertScort() {
    var result = new Result(
      new Date(),
      this.nombreQuestion,
      this.score,
      1,
      2
    );

    this.ResultService.insertResult(result).subscribe(result => {
      console.log(result);
    });
  }
}
