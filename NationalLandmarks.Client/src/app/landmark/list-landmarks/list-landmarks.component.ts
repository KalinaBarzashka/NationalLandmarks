import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { filter, map, Subscription } from 'rxjs';
import { Landmark } from 'src/app/models/Landmark';
import { Pagination } from 'src/app/models/Pagination';
import { AuthService } from 'src/app/services/auth.service';
import { LandmarkService } from 'src/app/services/landmark.service';

@Component({
  selector: 'app-list-landmarks',
  templateUrl: './list-landmarks.component.html',
  styleUrls: ['./list-landmarks.component.css']
})
export class ListLandmarksComponent implements OnInit, OnDestroy {
  landmarks!: Array<Landmark>;
  isLogged: boolean = false;
  pagingModel!: Pagination;
  status: string = 'INIT';
  subscription!: Subscription;

  constructor(
    private landmarkService: LandmarkService,
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthService,
    private toastrService: ToastrService) {
      this.subscription = router.events.pipe(
        // identify navigation end
        filter(e => e instanceof NavigationEnd),
        // get the activated route
        map(() => this.rootRoute(this.route)),
        filter((route: ActivatedRoute) => route.outlet === 'primary'),
      ).subscribe(event  => {
        var pageId = 1;
        if(!isNaN(Number(route.snapshot.paramMap.get('id')))) {
          pageId = Number(route.snapshot.paramMap.get('id'));
          this.fetchLandmarks(pageId);
        }
        
      });
    }

  ngOnInit(): void {
    this.status = 'LOADING';
    this.isLogged = this.authService.isAuthenticated();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  fetchLandmarks(id: number = 1) {
    this.status = 'LOADING';
    this.landmarkService.getLandmarks(id).subscribe(l => {
      this.landmarks = l.landmarks;
      this.pagingModel = {
        pageNumber: l.pageNumber,
        totalItemsCount: l.totalItemsCount,
        itemsPerPage: l.itemsPerPage,
        pagesCount: Math.ceil(l.totalItemsCount / l.itemsPerPage),
        hasPreviousPage: l.pageNumber > 1,
        previousPageNumber: l.pageNumber - 1,
        hasNextPage: l.pageNumber < Math.ceil(l.totalItemsCount / l.itemsPerPage),
        nextPageNumber: l.pageNumber + 1
      }
      this.status = 'SUCCESS';
    });
  }

  deleteLandmark(id: number): void {
    this.landmarkService.deleteLandmark(id).subscribe(res => {
      this.toastrService.success("Successfully deleted landmark!");
      this.fetchLandmarks();
    });
  }

  editLandmark(id: number) {
    this.router.navigate(['landmarks', id, 'edit']);
  }

  private rootRoute(route: ActivatedRoute): ActivatedRoute {
    while (route.firstChild) {
      route = route.firstChild;
    }
    return route;
  }

}
