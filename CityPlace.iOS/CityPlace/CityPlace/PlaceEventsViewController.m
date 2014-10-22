//
//  PlaceEventsViewController.m
//  CityPlace
//
//  Created by Yuri Korshev on 22.10.14.
//  Copyright (c) 2014 SoftGears. All rights reserved.
//

#import "PlaceEventsViewController.h"
#import "SWRevealViewController.h"
#import "CityPlaceCell.h"
#import "SDWebImage/UIImageView+WebCache.h"
#import "EventDetailsViewController.h"

@interface PlaceEventsViewController ()

@end

@implementation PlaceEventsViewController

@synthesize tableView;
@synthesize loadingIndicator;
@synthesize items;
@synthesize placeId;

- (void)viewDidLoad {
    [super viewDidLoad];
    
    self.tableView.hidden = YES;
    [self.loadingIndicator startAnimating];
    
    self.tableView.contentInset = UIEdgeInsetsMake(0, 0, 90, 0);
    
    UIBarButtonItem *item = [[UIBarButtonItem alloc] initWithImage:[UIImage imageNamed:@"nav"] style:UIBarButtonItemStylePlain target:self.revealViewController action:@selector(revealToggle:)];
    item.tintColor = [UIColor whiteColor];
    self.navigationItem.leftBarButtonItem = item;
    
    self.navigationItem.title = @"События";
    
    self.tableView.backgroundColor = [UIColor clearColor];
    [self.tableView.backgroundView setBackgroundColor:[UIColor clearColor]];
    
    NSString *url = [NSString stringWithFormat:@"http://cityplace.softgears.ru/mobile-api/places/events/%ld",(long)self.placeId];
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



#pragma mark - Navigation

// In a storyboard-based application, you will often want to do a little preparation before navigation
- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
    EventDetailsViewController *destViewController = (EventDetailsViewController*)segue.destinationViewController;
    
    NSIndexPath *myIndexPath = [self.tableView
                                indexPathForSelectedRow];
    
    NSDictionary *sectionObj = [self.items objectAtIndex:myIndexPath.section];
    
    NSObject *obj = [[sectionObj objectForKey:@"items"] objectAtIndex:myIndexPath.row];
    
    destViewController.navigationItem.title = [obj valueForKey:@"title"];
    NSNumber *eventId = [obj valueForKey:@"id"];
    destViewController.eventId = [eventId integerValue];
}


@end
