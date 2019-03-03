import { Component, Input, Output, EventEmitter, ViewChild, OnInit, AfterViewInit, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { HttpClient } from '@angular/common/http';

import { ScDialogService } from '@speak/ng-bcl/dialog';
import { ScAction } from '@speak/ng-bcl/action-control';

import { ScBizFxAction, ScBizFxView } from '@sitecore/bizfx';
import { ScBizFxActionComponent } from './sc-bizfx-action.component';
import { ScBizFxContextService, ScBizFxViewsService } from '@sitecore/bizfx';

@Component({
  selector: 'sc-bizfx-breadcrumb-new',
  templateUrl: './sc-bizfx-breadcrumb-new.component.html',
  styleUrls: ['./sc-bizfx-breadcrumb-new.component.css'],
  host: {
    '(document:click)': 'onClick($event)',
  }
})


export class ScBizFxBreadcrumbNewComponent implements AfterViewInit { 
  sub: any;
  currentRoute: string;

  constructor(
    protected http: HttpClient,
    protected router: Router,
    private _eref: ElementRef) {
      router.events.subscribe(val => {

        var newVar = val as any
        console.log(val);

        if (newVar.url.indexOf('entityView') < 0) 
        { 
          this.display = false; 
        }

        if(newVar.url.indexOf('Entity-Category') >= 0 
        || newVar.url.indexOf('Entity-SellableItem') >= 0 
        || newVar.url.indexOf('Entity-Catalog') >= 0 ) {
          this.display = true;
        } else {
          this.display = false;
        }    

        var pieces = newVar.url.split('/');
        var id =  pieces[pieces.length-1];

        if(this.display && this.currentId != id){
          this.currentId = id;
          this.loadItems();
        }

      })
    }

    public display = true;
    public breadCrumbs;
    public currentId = '';

  ngOnInit() {

  };

  ngAfterViewInit(): void {
   
  };
  
  @Input() view: ScBizFxView;
  
  onClick(event) {
    if (!this._eref.nativeElement.contains(event.target)){ 
      this.breadCrumbs.map((item, index) => (
        item.active = false
      ))
    }
  }

    loadItems(): void {     
  
      this.breadCrumbs = [
        {
          Name: 'Entity-Catalog-Habitat_Master',
          DisplayName: 'Habitat_Master',
          Href: '/entityView/Master/1/Entity-Catalog-Habitat_Master',
          IsActive: true,
          Icon: 'child.img',
          id: '1'
        },
        {
          Name: 'Entity-Catalog-Habitat_Master',
          DisplayName: 'Departments',
          Href: '/entityView/Master/1/Entity-Category-Habitat_Master-Departments',
          IsActive: false,
          Icon: 'child.img',
          id: '2'
        },
        {
          Name: 'Entity-Catalog-Habitat_Master',
          DisplayName: 'test',
          Href: '/entityView/Master/1/Entity-Catalog-Habitat_Master',
          IsActive: false,
          Icon: 'child.img',
          id: '3'
        }
      ]    
  
      this.http.get('api/GetBreadcrumbByItemId?itemId=' + this.currentId)
      .subscribe(
        response => {
          console.log('API answer: ', response);
        },
        err => {
          console.log('Server error: ' + JSON.stringify(err));          
        }
      )
       
      };

  loadSubItems(event, itemId): void {    

    var currentIndex = this.breadCrumbs.findIndex(item => item.id == itemId.id);

    this.breadCrumbs.forEach(element => {
      element.active = false ;
    });

    if(!itemId.children) {

      this.breadCrumbs[currentIndex].children = [
        {
          Name: 'Entity-Catalog-Habitat_Master',
          DisplayName: 'Child',
          Href: '/entityView/Master/1/Entity-Catalog-Habitat_Master',
          IsActive: true,
          Icon: 'child.img',
          id: '12'
        },
        {
          Name: 'Entity-Catalog-Habitat_Master',
          DisplayName: 'Child',
          Href: '/entityView/Master/1/Entity-Category-Habitat_Master-Departments',
          IsActive: false,
          Icon: 'child.img',
          id: '23'
        },
        {
          Name: 'Entity-Catalog-Habitat_Master',
          DisplayName: 'Child',
          Href: '/entityView/Master/1/Entity-Catalog-Habitat_Master',
          IsActive: false,
          Icon: 'child.img',
          id: '34'
        }
      ];
     
        this.http.get('api/GetChildrenByItemId?itemId=' + itemId.id)
        .subscribe(
          response => {
            console.log('API answer: ', response);                     
          },
          err => {
            console.log('Server error: ' + JSON.stringify(err));            
          }
        );

      this.breadCrumbs[currentIndex].active = !this.breadCrumbs[currentIndex].active;

    } else {
      this.breadCrumbs[currentIndex].active = !this.breadCrumbs[currentIndex].active;
    }
    
  }

  checkItemExists(item): boolean {
    return typeof(item.children) != "undefined"
  };

}