import {ModuleWithProviders} from '@angular/core';

export interface IBoardGame {
  boardGameId?: string;
  title: string;
  shortDescription: string;
  longDescription: string;
  price: number;
  imageThumbnailUri: string;
  imageFullUri: string;
  feedbacks: IFeedback[];
  boardGameGenre: IBoardGameGenre;
  // orders: IOrder[];
  // orderTotal?: number;
}

export interface IBoardGameGenre {
  boardGameGenreId: string;
  genreName: string;
  description: string;
}

export interface IFeedback {
  feedbackId: string;
  userName: string;
  message: string;
  boardGameId: string;
  boardGame: IBoardGame;
}

export interface IOrder {
  product: string;
  price: number;
  quantity: number;
  orderTotal?: number;
}

export interface IRouting {
  routes: ModuleWithProviders;
  components: any[];
}

export interface IPagedResults<T> {
  totalRecords: number;
  results: T;
}

export interface IBoardGameResponse {
  status: boolean;
  boardGame: IBoardGame;
}
