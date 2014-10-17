//
//  NewsViewController.h
//  CityPlace
//
//  Created by Yuri Korshev on 17.10.14.
//  Copyright (c) 2014 SoftGears. All rights reserved.
//

#import "BaseViewController.h"

@interface NewsViewController : BaseViewController <UITableViewDelegate, UITableViewDataSource>

@property (weak, nonatomic) IBOutlet UITableView *tableView;
@property (weak, nonatomic) IBOutlet UIActivityIndicatorView *loadingIndicator;
@property (nonatomic) NSMutableArray *items;

- (void) loadData;

@end
