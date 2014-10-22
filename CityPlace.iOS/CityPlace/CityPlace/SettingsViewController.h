//
//  SettingsViewController.h
//  CityPlace
//
//  Created by Yuri Korshev on 22.10.14.
//  Copyright (c) 2014 SoftGears. All rights reserved.
//

#import "BaseViewController.h"

@interface SettingsViewController : BaseViewController<UITableViewDataSource, UITableViewDataSource>

@property (weak, nonatomic) IBOutlet UITableView *tableView;

@end
