//
//  SettingsViewController.m
//  CityPlace
//
//  Created by Yuri Korshev on 22.10.14.
//  Copyright (c) 2014 SoftGears. All rights reserved.
//

#import "SettingsViewController.h"
#import "SWRevealViewController.h"
#import "SettingsTextCell.h"

@interface SettingsViewController ()



@end

@implementation SettingsViewController

@synthesize tableView;

- (void)viewDidLoad {
    [super viewDidLoad];
    
    // Конфигурирование окна
    self.navigationItem.title = @"Настройки";
    
    UIBarButtonItem *item = [[UIBarButtonItem alloc] initWithImage:[UIImage imageNamed:@"nav"] style:UIBarButtonItemStylePlain target:self.revealViewController action:@selector(revealToggle:)];
    item.tintColor = [UIColor whiteColor];
    self.navigationItem.leftBarButtonItem = item;
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (void) viewDidAppear:(BOOL)animated {
    [self.tableView reloadData];
}

- (NSInteger)numberOfSectionsInTableView:(UITableView *)tableView {
    // Return the number of sections.
    return 1;
}

- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    // Return the number of rows in the section.
    return 1;
}


- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    
    NSString *CellIdentifier = @"settingsCityCell";
    SettingsTextCell *cell = [self.tableView dequeueReusableCellWithIdentifier:CellIdentifier forIndexPath:indexPath];
    
    [cell.propertyValueText setText:[self getCityName]];
    
    return cell;
}

/*
#pragma mark - Navigation

// In a storyboard-based application, you will often want to do a little preparation before navigation
- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
    // Get the new view controller using [segue destinationViewController].
    // Pass the selected object to the new view controller.
}
*/

@end
