//
//  ContentViewController.m
//  CityPlace
//
//  Created by Yuri Korshev on 15.10.14.
//  Copyright (c) 2014 SoftGears. All rights reserved.
//

#import "ContentViewController.h"
#import "SWRevealViewController.h"
#import "CityPlaceCell.h"
#import "SDWebImage/UIImageView+WebCache.h"
#import "PlaceDetailsViewController.h"
#import "EventDetailsViewController.h"
#import "PubDetailsViewController.h"

@interface ContentViewController ()

@end

@implementation ContentViewController

@synthesize tableView, items, loadingIndicator;

- (void)viewDidLoad {
    [super viewDidLoad];    
    
    UIImage *image = [UIImage imageNamed:@"toolbar"];
    [self.navigationController.navigationBar setBackgroundImage:image forBarMetrics:UIBarMetricsDefault];
    
    SWRevealViewController *revealViewController = self.revealViewController;
    if ( revealViewController )
    {
        [self.revealButtonItem setTarget: self.revealViewController];
        [self.revealButtonItem setAction: @selector( revealToggle: )];
    }
    
    self.navigationItem.title = [self getCityName];
    self.tableView.hidden = YES;
    [self.loadingIndicator startAnimating];
    
    self.automaticallyAdjustsScrollViewInsets = NO;
    
    self.tableView.contentInset = UIEdgeInsetsMake(0, 0, 90, 0);
    
    NSString *url = [NSString stringWithFormat:@"http://cityplace.softgears.ru/mobile-api/home-data/%ld",(long)[self getCityId]];
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

- (UIStatusBarStyle)preferredStatusBarStyle
{
    return UIStatusBarStyleLightContent;
}

- (void) viewDidAppear:(BOOL)animated {
    
    SWRevealViewController *revealViewController = self.revealViewController;
    if ( revealViewController )
    {
        [self.view addGestureRecognizer: self.revealViewController.panGestureRecognizer];
    }
}

#pragma mark - Table view data source

- (NSInteger)numberOfSectionsInTableView:(UITableView *)tableView {
    // Return the number of sections.
    return [self.items count];
}

- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    // Return the number of rows in the section.
    NSDictionary *sectionObj = [self.items objectAtIndex:section];
    return [[sectionObj objectForKey:@"items"] count];
}


- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    
    CityPlaceCell *cell = [self.tableView dequeueReusableCellWithIdentifier:@"cpCell" forIndexPath:indexPath];
    
    NSDictionary *sectionObj = [self.items objectAtIndex:indexPath.section];
    
    NSObject *obj = [[sectionObj objectForKey:@"items"] objectAtIndex:indexPath.row];
    
    cell.label.text = [obj valueForKey:@"title"];
    cell.imageUrl = [obj valueForKey:@"img"];
    
    if (cell.imageUrl != nil){
        NSString *imageUrl = [NSString stringWithFormat:@"http://cityplace.softgears.ru/%@",cell.imageUrl];
        [cell.imageView sd_setImageWithURL:[NSURL URLWithString:imageUrl]
                          placeholderImage:[UIImage imageNamed:@"placeholder"]];
    }
    
    return cell;
}

- (NSString *)tableView:(UITableView *)tableView titleForHeaderInSection:(NSInteger)section
{
    NSDictionary *sectionObj = [self.items objectAtIndex:section];
    
    return [sectionObj valueForKey:@"key"];
}

-(UIView *) tableView:(UITableView *)tableView viewForHeaderInSection:(NSInteger)section {
    static NSString *CellIdentifier = @"cpHeader";
    CityPlaceCell *headerView = [self.tableView dequeueReusableCellWithIdentifier:CellIdentifier];
    
    NSDictionary *sectionObj = [self.items objectAtIndex:section];
    
    [headerView.label setText:[sectionObj valueForKey:@"key"]];
    
    
    return headerView;
}

- (CGFloat)tableView:(UITableView *)tableView heightForHeaderInSection:(NSInteger)section
{
    return 27;
}



- (CGFloat)tableView:(UITableView *)tableView heightForFooterInSection:(NSInteger)section
{
    return 0.01f;
}

-(UIView*)tableView:(UITableView*)tableView viewForFooterInSection:(NSInteger)section
{
    return nil;
}

- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath
{
    NSDictionary *sectionObj = [self.items objectAtIndex:indexPath.section];
	NSObject *obj = [[sectionObj objectForKey:@"items"] objectAtIndex:indexPath.row];
    NSString *objType = [obj valueForKey:@"objType"];
    NSString *objTitle = [obj valueForKey:@"title"];
    NSInteger objId = [[obj valueForKey:@"id"] integerValue];
    UIStoryboard *aStoryboard = [UIStoryboard storyboardWithName:@"Main" bundle:[NSBundle mainBundle]];
    if ([objType isEqualToString:@"place"]){        
        PlaceDetailsViewController *vc = [aStoryboard instantiateViewControllerWithIdentifier:@"PlaceDetailsViewController"];
        vc.placeId = objId;
        vc.navigationItem.title = objTitle;
        [self.navigationController pushViewController:vc animated:YES];
    }
    if ([objType isEqualToString:@"publication"]){
        PubDetailsViewController *vc = [aStoryboard instantiateViewControllerWithIdentifier:@"PublicationDetailsViewController"];
        vc.articleId = objId;
        vc.navigationItem.title = objTitle;
        [self.navigationController pushViewController:vc animated:YES];
    }
    if ([objType isEqualToString:@"event"]){
        EventDetailsViewController *vc = [aStoryboard instantiateViewControllerWithIdentifier:@"EventDetailsViewController"];
        vc.eventId = objId;
        vc.navigationItem.title = objTitle;
        [self.navigationController pushViewController:vc animated:YES];
    }
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
