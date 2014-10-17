//
//  NewsViewController.m
//  CityPlace
//
//  Created by Yuri Korshev on 17.10.14.
//  Copyright (c) 2014 SoftGears. All rights reserved.
//

#import "NewsViewController.h"
#import "SWRevealViewController.h"
#import "CityPlaceCell.h"
#import "SDWebImage/UIImageView+WebCache.h"

@interface NewsViewController ()



@end

@implementation NewsViewController

@synthesize items;
@synthesize tableView;
@synthesize loadingIndicator;

- (void)loadData {
    
    // Конфигурирование окна
    self.navigationItem.title = @"Новости города";
    
    UIBarButtonItem *item = [[UIBarButtonItem alloc] initWithImage:[UIImage imageNamed:@"nav"] style:UIBarButtonItemStylePlain target:self.revealViewController action:@selector(revealToggle:)];
    item.tintColor = [UIColor whiteColor];
    self.navigationItem.leftBarButtonItem = item;
    
    NSString *url = [NSString stringWithFormat:@"http://cityplace.softgears.ru/mobile-api/publications?cityId=%ld",(long)[self getCityId]];
    [self getJsonFromUrl:url success:^(id object){
        self.items = object;
        [self.tableView reloadData];
        [self.loadingIndicator stopAnimating];
        self.tableView.hidden = NO;
    }error: ^ (NSError * error){
        [self.loadingIndicator stopAnimating];
    }];
}

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view.
    
    self.tableView.hidden = YES;
    [self.loadingIndicator startAnimating];
    
    self.tableView.contentInset = UIEdgeInsetsMake(0, 0, 90, 0);
    
    [self loadData];
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

#pragma mark - Table view data source

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
    
    cell.label.text = [obj valueForKey:@"title"];
    cell.imageUrl = [obj valueForKey:@"img"];
    
    if (cell.imageUrl != nil){
        NSString *imageUrl = [NSString stringWithFormat:@"http://cityplace.softgears.ru/%@",cell.imageUrl];
        [cell.imageView sd_setImageWithURL:[NSURL URLWithString:imageUrl]
                       placeholderImage:[UIImage imageNamed:@"placeholder"]];
    }
    
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
