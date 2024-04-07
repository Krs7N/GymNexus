import { Component, Input, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { OrderProductDetailsModel } from '../../order-product-details-model';

@Component({
  selector: 'app-products-table',
  templateUrl: './products-table.component.html',
  styleUrls: ['./products-table.component.scss']
})
export class ProductsTableComponent implements OnInit {

  @Input() products: OrderProductDetailsModel[] = [];

  dataSource: MatTableDataSource<OrderProductDetailsModel>;
  displayedColumns: string[] = ['imageUrl', 'name', 'category', 'quantity', 'price'];

  constructor() {
    this.dataSource = new MatTableDataSource<OrderProductDetailsModel>;
  }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource<OrderProductDetailsModel>(this.products);
  }
}
