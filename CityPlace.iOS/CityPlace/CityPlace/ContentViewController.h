//
//  ContentViewController.h
//  CityPlace
//
//  Created by Yuri Korshev on 15.10.14.
//  Copyright (c) 2014 SoftGears. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "BaseViewController.h"

@interface ContentViewController : BaseViewController

@property (weak, nonatomic) IBOutlet UIBarButtonItem *revealButtonItem;
@property (weak, nonatomic) IBOutlet UIActivityIndicatorView *loadingIndicator;
@property (weak, nonatomic) IBOutlet UITableView *tableView;
@property (nonatomic) NSMutableArray *items;

@end
