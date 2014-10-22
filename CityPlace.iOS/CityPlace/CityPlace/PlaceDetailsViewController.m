//
//  PlaceDetailsViewController.m
//  CityPlace
//
//  Created by Yuri Korshev on 22.10.14.
//  Copyright (c) 2014 SoftGears. All rights reserved.
//

#import "PlaceDetailsViewController.h"
#import "SWRevealViewController.h"
#import "CityPlaceCell.h"
#import "SDWebImage/UIImageView+WebCache.h"
#import "PlaceImageCell.h"
#import "PlaceDescriptionCell.h"
#import "PlacePropCell.h"
#import "PlaceActionCell.h"
#import "PlaceEventsViewController.h"

@interface PlaceDetailsViewController ()

@property (nonatomic) NSMutableDictionary *obj;

@end

@implementation PlaceDetailsViewController

@synthesize tableView;
@synthesize loadingIndicator;
@synthesize items;
@synthesize placeId;

- (void)viewDidLoad {
    [super viewDidLoad];
    
    UIBarButtonItem *item = [[UIBarButtonItem alloc] initWithImage:[UIImage imageNamed:@"back"] style:UIBarButtonItemStylePlain target:self action:@selector(goBack)];
    item.tintColor = [UIColor whiteColor];
    self.navigationItem.leftBarButtonItem = item;
    
    self.tableView.hidden = YES;
    [self.loadingIndicator startAnimating];
    
    self.automaticallyAdjustsScrollViewInsets = NO;
    
    self.tableView.contentInset = UIEdgeInsetsMake(0, 0, 90, 0);
    
    [self loadData];
    
    self.items = @[@"placeImage",@"placeDescription",@"propCell",@"propCell",@"propCell",@"propCell",@"propCell",@"actionCell"];
}

- (void) goBack {
    [self.navigationController popViewControllerAnimated:YES];
}

- (void)loadData {
    
    NSString *url = [NSString stringWithFormat:@"http://cityplace.softgears.ru/mobile-api/place/%ld",(long)self.placeId];
    [self getJsonFromUrl:url success:^(id object){
        self.obj = object;
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
    
    NSString *CellIdentifier = [self.items objectAtIndex:indexPath.row];
    UITableViewCell *cell = [self.tableView dequeueReusableCellWithIdentifier:CellIdentifier forIndexPath:indexPath];
    
    switch (indexPath.row) {
        case 0:
            [self configureImageCell:cell];
            break;
        case 1:
            [self configureDescriptionCell:cell];
            break;
        case 2:
            [self configurePropCell:cell withTitle:@"Адрес" andValue:[self.obj valueForKey:@"address"]];
            break;
        case 3:
            [self configurePropCell:cell withTitle:@"Время работы" andValue:[self.obj valueForKey:@"work_time"]];
            break;
        case 4:
            [self configurePropCell:cell withTitle:@"Телефон" andValue:[self.obj valueForKey:@"phone1"]];
             break;
    	case 5:
             [self configurePropCell:cell withTitle:@"Сайт" andValue:[self.obj valueForKey:@"site"]];
            break;
        case 6:
            [self configurePropCell:cell withTitle:@"Средний чек" andValue:[NSString stringWithFormat:@"%@ руб.",[self.obj valueForKey:@"check"]]];
            break;
        case 7:
            [self configureActionCell:cell withTitle:@"Посмотреть события"];
            break;
            
        default:
            break;
    }
    
    return cell;
}

- (void) configureImageCell:(UITableViewCell*)cell {
    PlaceImageCell *imageCell = (PlaceImageCell*)cell;
    NSString * imageUrl = [NSString stringWithFormat:@"http://cityplace.softgears.ru/%@",[self.obj valueForKey:@"img"]];
    [imageCell.placeImage sd_setImageWithURL:[NSURL URLWithString:imageUrl] placeholderImage:[UIImage imageNamed:@"placeholder"]];
}

- (void) configureDescriptionCell:(UITableViewCell*)cell {
    PlaceDescriptionCell *descCell = (PlaceDescriptionCell*)cell;
    
    descCell.descLabel.text = [self.obj valueForKey:@"description"];
    CGSize labelSize = [self getLabelSize:descCell.descLabel.text];
    descCell.descLabel.frame = CGRectMake(descCell.descLabel.frame.origin.x, descCell.descLabel.frame.origin.y, labelSize.width, labelSize.height);
}

- (void) configureAddressCell:(UITableViewCell*)cell {
    PlaceDescriptionCell *descCell = (PlaceDescriptionCell*)cell;
    
    descCell.descLabel.text = [NSString stringWithFormat:@"Адрес: %@", [self.obj valueForKey:@"work_time"]];;
    CGSize labelSize = [self getLabelSize:descCell.descLabel.text];
    descCell.descLabel.frame = CGRectMake(descCell.descLabel.frame.origin.x, descCell.descLabel.frame.origin.y, labelSize.width, labelSize.height);
}

- (void) configureWorktimeCell:(UITableViewCell*)cell {
    PlaceDescriptionCell *descCell = (PlaceDescriptionCell*)cell;
    
    descCell.descLabel.text = [NSString stringWithFormat:@"Работаем: %@", [self.obj valueForKey:@"work_time"]];
    CGSize labelSize = [self getLabelSize:descCell.descLabel.text];
    descCell.descLabel.frame = CGRectMake(descCell.descLabel.frame.origin.x, descCell.descLabel.frame.origin.y, labelSize.width, labelSize.height);
}

- (void) configurePropCell:(UITableViewCell*)cell withTitle:(NSString*) title andValue:(NSString*)value {
    PlacePropCell *propCell = (PlacePropCell*)cell;
    propCell.propName.text = title;
    if (value == (id)[NSNull null] || value.length == 0 ) {
        value = @"";
    }
    propCell.propValue.text = value;
}

- (void) configureActionCell:(UITableViewCell*)cell withTitle:(NSString*) title {
    PlaceActionCell *actionCell = (PlaceActionCell*)cell;
    actionCell.actionTitle.text = title;
}



- (CGFloat)tableView:(UITableView *)tableView heightForRowAtIndexPath:(NSIndexPath *)indexPath{
    if(indexPath.row == 0)
    {
        return 300.0;
    }
    else if (indexPath.row == 1){
        CGSize labelSize = [self getLabelSize:[self.obj valueForKey:@"description"]];
        return labelSize.height+15;
    }
    else
    {
        return 31.0f;
    }
    return UITableViewAutomaticDimension;
}

- (CGSize) getLabelSize:(NSString*)string {
    NSString *cellText = string;
    if (cellText == nil){
        cellText = @"";
    }
    UIFont *cellFont = [UIFont fontWithName:@"Helvetica Neue" size:17.0];
    CGSize constraintSize = CGSizeMake(self.view.frame.size.width, MAXFLOAT);
    CGSize labelSize = [cellText sizeWithFont:cellFont constrainedToSize:constraintSize lineBreakMode:UILineBreakModeWordWrap];
    return labelSize;
}



#pragma mark - Navigation

// In a storyboard-based application, you will often want to do a little preparation before navigation
- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
    if ([segue.identifier isEqualToString:@"showPlaceEvents"]){
        PlaceEventsViewController *destViewController = (PlaceEventsViewController*)segue.destinationViewController;
        
        destViewController.navigationItem.title = [NSString stringWithFormat:@"События %@",[self.obj valueForKey:@"title"]];
        NSNumber *plId = [self.obj valueForKey:@"id"];
        destViewController.placeId = [plId integerValue];
    }
}


@end
