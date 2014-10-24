//
//  PubDetailsViewController.m
//  CityPlace
//
//  Created by Yuri Korshev on 20.10.14.
//  Copyright (c) 2014 SoftGears. All rights reserved.
//

#import "PubDetailsViewController.h"
#import "SDWebImage/UIImageView+WebCache.h"
#import <QuartzCore/QuartzCore.h>


@interface PubDetailsViewController ()

@property (weak, nonatomic) IBOutlet UIScrollView *scrollView;
@property (weak, nonatomic) IBOutlet UIActivityIndicatorView *loadingIndicator;
@property (weak, nonatomic) IBOutlet UIImageView *imageView;
@property (weak, nonatomic) IBOutlet UILabel *detailsText;
@property (weak, nonatomic) IBOutlet UILabel *publishedText;

@end

@implementation PubDetailsViewController

@synthesize articleId;

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
    
    NSString *url = [NSString stringWithFormat:@"http://cityplace.softgears.ru/mobile-api/publication/%ld",(long)self.articleId];
    [self getJsonFromUrl:url success:^(id object){
        [self.loadingIndicator stopAnimating];
        NSString * imageUrl = [NSString stringWithFormat:@"http://cityplace.softgears.ru/%@",[object valueForKey:@"img"]];
        [self.imageView sd_setImageWithURL:[NSURL URLWithString:imageUrl]
                          placeholderImage:[UIImage imageNamed:@"placeholder"]];
        [self.detailsText setText:[object valueForKey:@"content"]];
        
        [self.publishedText setText:[NSString stringWithFormat:@"Опубликовано -  %@",[object valueForKey:@"pdate"]]];
        self.scrollView.hidden = NO;
        
        [self.detailsText sizeToFit];
        [self.view updateConstraints];
        //CGFloat height = self.publishedText.frame.size.height;
        //[self.scrollView setContentSize:CGSizeMake(self.view.frame.size.width, self.publishedText.frame.origin.y+height)];
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
    //CGFloat height = self.publishedText.frame.size.height;
    //[self.scrollView setContentSize:CGSizeMake(self.view.frame.size.width, self.publishedText.frame.origin.y+height)];
    
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
