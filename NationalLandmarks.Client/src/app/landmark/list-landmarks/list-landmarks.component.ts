import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { filter, map, Subscription } from 'rxjs';
import { Landmark } from 'src/app/models/Landmark';
import { Pagination } from 'src/app/models/Pagination';
import { AuthService } from 'src/app/services/auth.service';
import { LandmarkService } from 'src/app/services/landmark.service';
import jwt_decode from 'jwt-decode';

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
  userId: string = '';

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
        if(!isNaN(Number(route.snapshot.params['id']))) {
          pageId = Number(route.snapshot.params['id']);
          if(pageId == 0) {
            pageId = 1;
          }
        }
        this.fetchLandmarks(pageId);
      });

      const tokenString = this.authService.getToken();
      if(tokenString == null) {
        return;
      }

      const tokenInfo = this.getDecodedAccessToken(tokenString); // decode token
      this.userId = tokenInfo.nameid;
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

  deleteLandmark(id: number, pageNumber: number = 1): void {
    this.landmarkService.deleteLandmark(id).subscribe(res => {
      this.toastrService.success("Successfully deleted landmark!");
      this.fetchLandmarks(pageNumber);
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

  private getDecodedAccessToken(token: string): any {
    try {
      return jwt_decode(token);
    } catch(Error) {
      return null;
    }
  }

}
