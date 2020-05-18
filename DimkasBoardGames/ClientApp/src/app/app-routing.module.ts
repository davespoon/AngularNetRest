import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';

import {HomeComponent} from './home/home.component';
import {BoardGamesComponent} from './boardGames/boardGames.component';
import {BoardGamesGridComponent} from './boardGames/boardGames-grid.component';
// import {BoardGameEditComponent} from './boardGames/boardGame-edit.component';
// import {BoardGameEditReactiveComponent} from './boardGames/boardGame-edit-reactive.component';

const routes: Routes = [
  {path: '', pathMatch: 'full', redirectTo: '/boardGames'},
  // {path: 'home', component: HomeComponent},
  {path: '**', redirectTo: '/boardGames'},
  {path: 'boardGames', component: BoardGamesComponent},
  // { path: 'boardGames/:id', component: BoardGameEditReactiveComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
  static components = [HomeComponent, BoardGamesComponent, BoardGamesGridComponent
        // BoardGamesGridComponent, BoardGameEditComponent, BoardGameEditReactiveComponent
  ];
}
