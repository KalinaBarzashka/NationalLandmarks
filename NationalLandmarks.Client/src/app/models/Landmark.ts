export interface Landmark {
  id: number;
  name: string;
  isNationalLandmark: boolean;
  description: string;
  townId: number;
  townName: string;
  address: string;
  latitude: string;
  longitude: string;
  imageUrl: string;
  opens: string;
  closes: string;
  worksOnWeekends: boolean;
  email: string;
  phone: string;
  website: string;
  userId: string;
  userName: string;
  visitsCount: number;
  totalVisits: number;
  grades: number[];
}