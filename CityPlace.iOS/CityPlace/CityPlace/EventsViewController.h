//
//  EventsViewController.h
//  CityPlace
//
//  Created by Yuri Korshev on 21.10.14.
//  Copyright (c) 2014 SoftGears. All rights reserved.
//

#import "BaseViewController.h"

@interface EventsViewController : BaseViewController<UITabBarDelegate,UITableViewDataSource>

@property (weak, nonatomic) IBOutlet UITableView *tableView;
@property (weak, nonatomic) IBOutlet UIActivityIndicatorView *loadingIndicator;
@property (nonatomic) NSMutableArray *items;

@end
