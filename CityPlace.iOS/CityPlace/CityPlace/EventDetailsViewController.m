//
//  EventDetailsViewController.m
//  CityPlace
//
//  Created by Yuri Korshev on 21.10.14.
//  Copyright (c) 2014 SoftGears. All rights reserved.
//

#import "EventDetailsViewController.h"
#import "SDWebImage/UIImageView+WebCache.h"
#import <QuartzCore/QuartzCore.h>
#import "PlaceDetailsViewController.h"

@interface EventDetailsViewController ()

@property (weak, nonatomic) IBOutlet UIScrollView *scrollView;
@property (weak, nonatomic) IBOutlet UIActivityIndicatorView *loadingIndicator;
@property (weak, nonatomic) IBOutlet UIImageView *imageView;
@property (weak, nonatomic) IBOutlet UILabel *detailsText;
@property (weak, nonatomic) IBOutlet UILabel *startDateText;
@property (weak, nonatomic) IBOutlet UILabel *endDateText;
@property (weak, nonatomic) IBOutlet UIButton *whereButton;
@property (nonatomic) id obj;

@end

@implementation EventDetailsViewController

@synthesize eventId;

- (void)viewDidLoad {
    [super viewDidLoad];
    
    UIBarButtonItem *item = [[UIBarButtonItem alloc] initWithImage:[UIImage imageNamed:@"back"] style:UIBarButtonItemStylePlain target:self action:@selector(goBack)];
    item.tintColor = [UIColor whiteColor];
    self.navigationItem.leftBarButtonItem = item;
    
    [self.loadingIndicator startAnimating];
    self.scrollView.hidden = YES;
    
    self.imageView.layer.backgroundColor=[[UIColor clearColor] CGColor];
    self.imageView.layer.cornerRadius=150.0f;
    self.imageView.layer.borderWidth=1.0;
    self.imageView.layer.masksToBounds = YES;
    self.imageView.layer.borderColor=[[UIColor whiteColor] CGColor];
    
    self.detailsText.preferredMaxLayoutWidth = self.detailsText.frame.size.width;
    self.scrollView.contentInset = UIEdgeInsetsMake(0, 0, 90, 0);
    
    NSString *url = [NSString stringWithFormat:@"http://cityplace.softgears.ru/mobile-api/event/%ld",(long)self.eventId];
    [self getJsonFromUrl:url success:^(id object){
        [self.loadingIndicator stopAnimating];
        NSString * imageUrl = [NSString stringWithFormat:@"http://cityplace.softgears.ru/%@",[object valueForKey:@"img"]];
        [self.imageView sd_setImageWithURL:[NSURL URLWithString:imageUrl]
                          placeholderImage:[UIImage imageNamed:@"placeholder"]];
        [self.detailsText setText:[object valueForKey:@"description"]];
        
        [self.startDateText setText:[NSString stringWithFormat:@"Начало -  %@",[object valueForKey:@"event_start"]]];
        
        [self.endDateText setText:[NSString stringWithFormat:@"Конец   -  %@",[object valueForKey:@"event_end"]]];
        
        [self.whereButton setTitle:[NSString stringWithFormat:@"Где - %@",[object valueForKey:@"placeName"]] forState:UIControlStateNormal];
        
        self.scrollView.hidden = NO;
        
        self.obj = object;
        
        //[self.detailsText sizeToFit];
        
        [self.view updateConstraints];
        
    }error: ^ (NSError * error){
        [self.loadingIndicator stopAnimating];
    }];
}

- (void) goBack {
    [self.navigationController popViewControllerAnimated:YES];
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (void) viewDidLayoutSubviews {
    
}


#pragma mark - Navigation

// In a storyboard-based application, you will often want to do a little preparation before navigation
- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
    PlaceDetailsViewController *destViewController = (PlaceDetailsViewController*)segue.destinationViewController;
    
    destViewController.navigationItem.title = [self.obj valueForKey:@"placeName"];
    NSNumber *selPlaceId = [self.obj valueForKey:@"placeId"];
    destViewController.placeId = [selPlaceId integerValue];
}


@end
