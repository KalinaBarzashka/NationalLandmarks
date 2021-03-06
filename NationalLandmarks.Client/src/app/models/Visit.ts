export interface Visit {
  landmarkId: number;
  landmark: VisitLandmark;
  grade: number;
  visitedOn: string;
}

export interface VisitLandmark {
  name: string;
  isNationalLandmark: boolean;
  townName: string;
  imageUrl: string;
}