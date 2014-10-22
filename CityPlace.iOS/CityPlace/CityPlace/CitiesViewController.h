//
//  CitiesViewController.h
//  CityPlace
//
//  Created by Yuri Korshev on 22.10.14.
//  Copyright (c) 2014 SoftGears. All rights reserved.
//

#import "BaseViewController.h"

@interface CitiesViewController : BaseViewController<UITableViewDataSource,UITableViewDataSource>

@property (weak, nonatomic) IBOutlet UITableView *tableView;
@property (weak, nonatomic) IBOutlet UIActivityIndicatorView *loadingIndicator;
@property (nonatomic) NSMutableArray *items;

@end
