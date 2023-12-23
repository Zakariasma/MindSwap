import {Component, OnInit} from '@angular/core';
import {ResultService} from "../../_services/result.service";
import {Historique} from "../../models/historique";

@Component({
  selector: 'app-historique',
  templateUrl: './historique.component.html',
  styleUrls: ['./historique.component.css']
})
export class HistoriqueComponent implements OnInit {
  results!: Historique[];
  constructor(private resultService: ResultService) { }

  ngOnInit(): void {
    this.resultService.getResults(2).subscribe(results => {
      this.results = results;
      console.log(this.results);
    });
  }

  formatDate(date: Date): string {
    const options: Intl.DateTimeFormatOptions = { day: '2-digit', month: '2-digit', year: 'numeric' };
    return new Date(date).toLocaleDateString('fr-FR', options);
  }
}
