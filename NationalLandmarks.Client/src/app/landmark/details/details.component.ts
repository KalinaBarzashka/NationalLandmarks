import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map, mergeMap } from 'rxjs';
import { Landmark } from 'src/app/models/Landmark';
import { LandmarkService } from 'src/app/services/landmark.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {
  id!: number;
  landmark!: Landmark;
  constructor(private route: ActivatedRoute, private landmarkService: LandmarkService) {
    this.fetchData();
  }

  fetchData(): void {
    this.route.params.pipe(
      map(params => {
        const id = params['id'];
        return id;
      }), 
      mergeMap(id => this.landmarkService.getLandmark(id))//switchMap()
    ).subscribe(res => {
      this.landmark = res;
    });
  }

  ngOnInit(): void {
  }

}
