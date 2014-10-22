//
//  CitiesViewController.m
//  CityPlace
//
//  Created by Yuri Korshev on 22.10.14.
//  Copyright (c) 2014 SoftGears. All rights reserved.
//

#import "CitiesViewController.h"
#import "CityPlaceCell.h"
#import "SettingsViewController.h"

@interface CitiesViewController ()

@end

@implementation CitiesViewController

@synthesize tableView;
@synthesize loadingIndicator;
@synthesize items;

- (void)viewDidLoad {
    [super viewDidLoad];
    
    self.navigationItem.title = @"Выберите город";
    
    UIBarButtonItem *item = [[UIBarButtonItem alloc] initWithImage:[UIImage imageNamed:@"back"] style:UIBarButtonItemStylePlain target:self action:@selector(goBack)];
    item.tintColor = [UIColor whiteColor];
    self.navigationItem.leftBarButtonItem = item;
    
    self.tableView.hidden = YES;
    [self.loadingIndicator startAnimating];
    
    self.automaticallyAdjustsScrollViewInsets = NO;
    
    self.tableView.contentInset = UIEdgeInsetsMake(0, 0, 90, 0);
    
    [self loadData];
}

- (void) goBack {
    [self.navigationController popViewControllerAnimated:YES];
}

- (void)loadData {
    
    NSString *url = @"http://cityplace.softgears.ru/mobile-api/cities/";
    [self getJsonFromUrl:url success:^(id object){
        self.items = object;
        [self.tableView reloadData];
        [self.loadingIndicator stopAnimating];
        self.tableView.hidden = NO;
    }error: ^ (NSError * error){
        [self.loadingIndicator stopAnimating];
    }];
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (NSInteger)numberOfSectionsInTableView:(UITableView *)tableView {
    // Return the number of sections.
    return 1;
}

- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    // Return the number of rows in the section.
    return [items count];
}


- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    
    CityPlaceCell *cell = [self.tableView dequeueReusableCellWithIdentifier:@"cpCell" forIndexPath:indexPath];
    
    NSObject *obj = [self.items objectAtIndex:indexPath.row];
    
    cell.label.text = [obj valueForKey:@"name"];
    
    NSString *currentCity = [self getCityName];
    
    if ([cell.label.text isEqualToString:currentCity]){
        [cell setAccessoryType:UITableViewCellAccessoryCheckmark];
    }
    
    return cell;
}

- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath
{
    NSObject *obj = [self.items objectAtIndex:indexPath.row];
    [self updateCurrentCityWithId:[obj valueForKey:@"id"] andName:[obj valueForKey:@"name"]];
    [self.navigationController popViewControllerAnimated:YES];
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
